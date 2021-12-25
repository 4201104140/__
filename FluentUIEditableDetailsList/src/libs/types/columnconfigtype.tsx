import { IColumn } from '@fluentui/react/lib/components/DetailsList/DetailsList.types';

import { ICellStyleRulesType } from './cellstyleruletype';

export interface IColumnConfig extends IColumn {
  key: string;
  text: string;
  cellStyleRule?: ICellStyleRulesType;
}