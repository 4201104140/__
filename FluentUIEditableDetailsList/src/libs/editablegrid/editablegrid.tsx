import * as React from 'react';
import { ColumnActionsMode, ConstrainMode, IColumn, IDetailsHeaderProps } from '@fluentui/react/lib/components/DetailsList/DetailsList.types';
import { useState, useEffect } from 'react';
import { DetailsList, IDetailsListProps } from '@fluentui/react/lib/components/DetailsList/DetailsList';
import { CommandBar, ICommandBarItemProps } from '@fluentui/react/lib/CommandBar';
import { DetailsListLayoutMode,
  Selection,
  SelectionMode,
  IObjectWithKey,
  IDetailsColumnRenderTooltipProps, } from '@fluentui/react/lib/DetailsList';
import { MarqueeSelection } from '@fluentui/react/lib/MarqueeSelection';

import { InitializeInternalGrid } from './editablegridinitialize';

import { Props } from '../types/editabledetailslistprops';

import { PrimaryButton, Panel, PanelType, IStackTokens, Stack, mergeStyleSets, Fabric, Dropdown, IDropdownStyles, IDropdownOption, IButtonStyles, DialogFooter, Announced, Dialog, SpinButton, DefaultButton, DatePicker, IDatePickerStrings, on, ScrollablePane, ScrollbarVisibility, Sticky, StickyPositionType, IRenderFunction, TooltipHost, mergeStyles, Spinner, SpinnerSize, TagPicker, ITag, IBasePickerSuggestionsProps, IInputProps } from 'office-ui-fabric-react';

const EditableGrid = (props: Props) => {
  const [defaultGridData, setDefaultGridData] = useState<any[]>([]);
  const [selectionDetails, setSelectionDetails] = useState('');
  const [selectionCount, setSelectionCount] = useState(0);

  let _selection: Selection = new Selection({
    onSelectionChanged: () => setSelectionDetails(_getSelectionDetails()),
  });

  useEffect(() => {
    if(props && props.items){
      var data : any[] = InitializeInternalGrid(props.items);
      SetGridItems(data);
    }
  })

  const SetGridItems = (data: any[]) : void => {
    setDefaultGridData(data);
  }

  const CreateColumnConfigs = () : IColumn[] => {

    let columnConfigs: IColumn[] = [];

    props.columns.forEach((column, index) => {
      var colHeaderClassName = 'id-' + props.id + '-col-' + index;
      var colKey = 'col' + index;
      columnConfigs.push({
        key: colKey,
        name: column.text,
        headerClassName: colHeaderClassName,
        ariaLabel: column.text,
        fieldName: column.key,
        isResizable: true,
        minWidth: column.minWidth,
        maxWidth: column.maxWidth,
        onRender: column.onRender ? column.onRender : (item, rowNum) => {

        }
      })
    });
    return columnConfigs;
  }

  const CreateCommandBarItemProps = () : ICommandBarItemProps[] => {
    let commandBarItems: ICommandBarItemProps[] = [];

    if (true) {
      commandBarItems.push({
        id: 'export',
        key: 'exportGrid',
        text: 'Export',
        ariaLabel: 'Export',
        display: false,
        iconProps: { iconName: 'Download' },

      })
    }

    return commandBarItems;
  }

  const GridColumns = CreateColumnConfigs();

  const CommandBarItemProps = CreateCommandBarItemProps();

  function _getSelectionDetails(): string {
    const count = _selection.getSelectedCount();
    setSelectionCount(count);

    switch (count) {
      case 0:
        return 'No items selected';
      case 1:
        return '1 item selected: ';
      default:
        return `${count} items selected`;
    }
  }

  return (
    <Fabric>
      <Panel>

      </Panel>
      <CommandBar items={CommandBarItemProps}/>
      <div className={mergeStyles({ height: '70vh', width: '130vh',})}>
        <ScrollablePane scrollbarVisibility={ScrollbarVisibility.auto}>
          <MarqueeSelection selection={_selection}>
            <DetailsList
              compact={true}
              items={defaultGridData.length > 0 ? defaultGridData : []}
              columns={GridColumns}
            />
          </MarqueeSelection>
        </ScrollablePane>
      </div>
    </Fabric>
  );
};

export default EditableGrid;