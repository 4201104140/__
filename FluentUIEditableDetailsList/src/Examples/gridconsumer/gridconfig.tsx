import { IColumnConfig } from "../../libs/types/columnconfigtype";

export const GridColumnConfig : IColumnConfig[] =
[
  {
    key: 'id',
    name: 'ID',
    text: 'ID',
    minWidth: 100,
    maxWidth: 100,
    isResizable: true,
  },
  {
    key: 'name',
    name: 'Name',
    text: 'Name',
    minWidth: 100,
    maxWidth: 100,
    isResizable: true,
  },
  {
    key: 'age',
    name: 'Age',
    text: 'Age',
    minWidth: 100,
    maxWidth: 100,
    isResizable: true,
  }
]

export interface GridItemsType {
  id: number;
  name: string;
  age: number;
  
}