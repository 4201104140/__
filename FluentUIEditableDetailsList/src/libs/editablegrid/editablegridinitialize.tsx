import { IColumn, ICommandBarItemProps, IconButton, TextField } from "@fluentui/react";
import React from "react";

import { initializeIcons } from '@fluentui/react';

initializeIcons(/* optional base url */);

export const InitializeInternalGrid = (items: any[]) : any[] => {
  return items.map((obj, index) => {
    if(Object.keys(obj).indexOf('_grid_row_id_') == -1 && Object.keys(obj).indexOf('_grid_row_operation_') == -1)
    {
      obj._grid_row_id_ = index;
      obj._grid_row_operation_ = undefined;
      obj._is_filtered_in_ = true;
      obj._is_filtered_in_grid_search_ = true;
      obj._is_filtered_in_column_filter_ = true;
    }
    return obj;
  })
};

export const ResetGridRowID = (items : any[]) : any[] => {
  return items.map((obj, index) => {
    return obj;
  });
};

export const InitializeInternalGridEditStructure = (items: any[]) : any[] => {
  let activateCellEditTmp : any[] = [];
  items.forEach((item, index) => {
    let activateCellEditRowTmp : any = {'isActivated' : false, properties : {}};

    activateCellEditTmp.push(activateCellEditRowTmp);
  });

  return activateCellEditTmp;
}