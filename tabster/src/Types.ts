/*!
 * Copyright (c) Microsoft Corporation. All rights reserved.
 * Licensed under the MIT License.
 */

export const TabsterAttributeName = "data-tabster";

export interface TabsterCoreProps {
    /**
     * Allows all tab key presses under the tabster root to be controlled by tabster
     * @default true
     */
     controlTab?: boolean;
}
    

export interface TabsterCore extends Pick<TabsterCoreProps, "controlTab"> {
    focusable: FocusableAPI;
}

export interface FocusableProps {
    isDefault?: boolean;
    isIgnored?: boolean;
    /**
     * Do not determine an element's focusability based on aria-disabled
     */
    ignoreAriaDisabled?: boolean;
}

export interface FindFocusableProps {
    /**
     * The container used for the search
     */
    container?: HTMLElement;
    /**
     * The elemet to start from
     */
    currentElement?: HTMLElement;
    /**
     * includes elements that can be focused programmatically
     */
    includeProgrammaticallyFocusable?: boolean;
    ignoreGroupper?: boolean;
    /**
     * Ignore uncontrolled areas.
     */
    ignoreUncontrolled?: boolean;
    /**
     * Ignore accessibility check.
     */
    ignoreAccessibiliy?: boolean;
    prev?: boolean;
    /**
     * @param el element visited
     * @returns if an element should be accepted
     */
    acceptCondition?(el: HTMLElement): boolean;
    /**
     * A callback that will be called if an uncontrolled area is met.
     * @param el uncontrolled element.
     */
    onUncontrolled?(el: HTMLElement): void;
}

export type FindFirstProps = Pick<
    FindFocusableProps,
    | "container"
    | "includeProgrammaticallyFocusable"
    | "ignoreGroupper"
    | "ignoreUncontrolled"
    | "ignoreAccessibiliy"
>;

export interface FocusableAPI {
    getProps(element: HTMLElement): FocusableProps;

    findFirst(options: FindFirstProps): HTMLElement | null | undefined;
}

export interface ModalizerProps {
    id: string;
    isOthersAccessible?: boolean;
    isAlwaysAccessible?: boolean;
    isNoFocusFirst?: boolean;
    isNoFocusDefault?: boolean;
}

export type TabsterAttributeProps = Partial<{
    modalizer: ModalizerProps;
}>;