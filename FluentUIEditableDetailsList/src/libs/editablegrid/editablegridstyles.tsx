import { getTheme, IDetailsColumnStyles, IDropdownStyles, IStackStyles, IStackTokens, ITextFieldStyles, mergeStyleSets } from "@fluentui/react";

import { IColumnConfig } from "../types/columnconfigtype";

export const stackStyles: Partial<IStackStyles> = { root: { width: 500 } };

export const controlClass = mergeStyleSets({
  control: {
    marginBottom: '10px',
  }
});

export const GetDynamicSpanStyles = (column: IColumnConfig, cellValue: number | string | undefined) : string => {

  var styleRule = column.cellStyleRule ?? undefined;
  var isRuleTrue : boolean = false;
  var styles = mergeStyleSets({
    dynamicSpanStyle: {
      display: 'inline-block',
      width: '100%',
      height: '100%',
      color: undefined,
    }
  });
  return styles.dynamicSpanStyle;
}

export const verticalGapStackTokens: IStackTokens = {
  childrenGap: 15,
  padding: 10,
};

export const horizontalGapStackTokens: IStackTokens = {
  childrenGap: 10,
  padding: 10,
};

export const textFieldStyles: Partial<ITextFieldStyles> = { fieldGroup: {} };

export const dropdownStyles: Partial<IDropdownStyles> = {
  dropdown: { width: '90%' },
};