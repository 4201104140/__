import { WindowWithTabsterInstance } from "./Root";
import * as Types from "./Types";

export { Types };

export function getTabsterAttribute(
    props: Types.TabsterAttributeProps,
    plain: true
): string;
export function getTabsterAttribute(
    props: Types.TabsterAttributeProps,
    plain?: true
): string {
    const attr = JSON.stringify(props);

    if (plain === true) {
        return attr;
    }

    return "";
}
/**
 * Returns an instance of Tabster if it already exists on the window .
 * @param win window instance that could contain an Tabster instance.
 */
 export function getCurrentTabster(win: Window): Types.TabsterCore | undefined {
    return (win as WindowWithTabsterInstance).__tabsterInstance;
}