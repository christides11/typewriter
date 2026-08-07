using Aarthificial.Typewriter.Editor.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Aarthificial.Typewriter.Editor.Layout {
  [UxmlElement]
  public partial class TabbedView : VisualElement {
    private readonly VisualElement _tabContainer;
    private List<VisualElement> _panes;
    private List<ToolbarToggle> _tabs;

    public TabbedView() {
      var visualTree =
        Resources.Load<VisualTreeAsset>("UXML/Layout/TabbedView");
      visualTree.CloneTree(this);

      _tabContainer = this.Q<VisualElement>("tabs");
      RegisterCallback<AttachToPanelEvent>(HandleAttachToPanel);
    }

    [UxmlAttribute]
    public bool SelectAny { get; set; }
    public event Action<int> TabChanged;

    private void HandleAttachToPanel(AttachToPanelEvent evt) {
      Refresh();
    }

    public void Refresh() {
      _tabContainer.Clear();
      _panes = Children().Skip(1).ToList();
      _tabs = _panes.Select(
          element => {
            var tab = new ToolbarToggle();
            tab.AddToClassList("inspector-tab");
            tab.text = element.name;
            tab.viewDataKey = element.name + "-tab";
            tab.RegisterValueChangedCallback(HandleChangedTab);
            if (element is EditableListView listView) {
              tab.tooltip = listView.TitleTooltip;
            }

            _tabContainer.Add(tab);
            element.style.display =
              tab.value ? DisplayStyle.Flex : DisplayStyle.None;
            return tab;
          }
        )
        .ToList();

      if (SelectAny && !_tabs.Any(element => element.value)) {
        ToggleTab(0, true);
      }
    }

    private void HandleChangedTab(ChangeEvent<bool> evt) {
      var index = _tabs.IndexOf(evt.target as ToolbarToggle);
      if (index < 0) {
        return;
      }

      ToggleTab(index, evt.newValue);
    }

    public void ToggleTab(int index, bool value) {
      if (value) {
        for (var i = 0; i < _tabs.Count; i++) {
          _tabs[i].SetValueWithoutNotify(index == i);
          _panes[i].style.display =
            index == i ? DisplayStyle.Flex : DisplayStyle.None;
        }
      } else {
        if (SelectAny) {
          _tabs[index].SetValueWithoutNotify(true);
        } else {
          _panes[index].style.display = DisplayStyle.None;
        }
      }

      TabChanged?.Invoke(value ? index : -1);
    }

  }
}
