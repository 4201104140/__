import * as Types from "./Types";

export interface WindowWithTabsterInstance extends Window {
    __tabsterInstance?: Types.TabsterCore;
}