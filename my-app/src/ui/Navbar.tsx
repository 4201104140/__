import * as React from 'react';
import { Nav, INavStyles, INavLinkGroup , INavProps, INavState} from '@fluentui/react/lib/Nav';
import { INavLink } from '@fluentui/react';

const navStyles: Partial<INavStyles> = { root: { width: 300 } };

let selectKey = 'ActivityItem';

const navLinkGroups: INavLinkGroup[] = [
  {
    name: 'Basic components',
    expandAriaLabel: 'Expand Basic components section',
    collapseAriaLabel: 'Collapse Basic components section',
    links: [
      {
        key: 'ActivityItem',
        name: 'ActivityItem',
        url: '/examples/activityitem',
				ariaCurrent: 'location',
      },
      {
        key: 'Breadcrumb',
        name: 'Breadcrumb',
        url: '/examples/breadcrumb',
      },
      {
        key: 'Button',
        name: 'Button',
        url: '#/examples/button',
      },
    ],
  },
  {
    name: 'Extended components',
    expandAriaLabel: 'Expand Extended components section',
    collapseAriaLabel: 'Collapse Extended components section',
    links: [
      {
        key: 'ColorPicker',
        name: 'ColorPicker',
        url: '#/examples/colorpicker',
      },
      {
        key: 'ExtendedPeoplePicker',
        name: 'ExtendedPeoplePicker',
        url: '#/examples/extendedpeoplepicker',
      },
      {
        key: 'GroupedList',
        name: 'GroupedList',
        url: '#/examples/groupedlist',
      },
    ],
  },
  {
    name: 'Utilities',
    expandAriaLabel: 'Expand Utilities section',
    collapseAriaLabel: 'Collapse Utilities section',
    links: [
      {
        key: 'FocusTrapZone',
        name: 'FocusTrapZone',
        url: '#/examples/focustrapzone',
      },
      {
        key: 'FocusZone',
        name: 'FocusZone',
        url: '#/examples/focuszone',
      },
      {
        key: 'MarqueeSelection',
        name: 'MarqueeSelection',
        url: '#/examples/marqueeselection',
      },
    ],
  },
];

export class Navbar extends React.Component<INavProps, INavState> {
	constructor(props: INavProps) {
		super(props);

		this.state = {
			isGroupCollapsed: {
				ActivityItem: true
			},
			selectedKey: 'ActivityItem'
		}
	}

	render() {
		return (
			<Nav 
				selectedKey=''
				ariaLabel=''
				styles={navStyles}
				groups={navLinkGroups}
			/>
		);
	}
}

function _onLinkClick(ev?: React.MouseEvent<HTMLElement>, item?: INavLink) {
	selectKey = item?.key ?? ""
}