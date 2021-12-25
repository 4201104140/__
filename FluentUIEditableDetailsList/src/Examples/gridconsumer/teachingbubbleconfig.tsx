import { ITeachingBubbleProps } from '@fluentui/react';

export interface ITeachingBubblePropsExtended extends ITeachingBubbleProps{
  innerText: string;
  isWide?: boolean;
}

export const teachingBubbleConfig : ITeachingBubblePropsExtended[] = [
  {
    target: '#searchField',
    headline: 'Grid Search',
    innerText: `Enter text to Search the grid across \"searchable\" columns. 
                Searchable columns are columns that have \'includeColumnInSearch: true\'. 
                Search is case-insensitive.`
  },
  {
    target: '#tutorialinfo',
    headline: 'Tutorial',
    innerText: 'Click me to view tutorial',
    isWide: false
  }
]

export interface ITeachingBubbleConfig {
  id: number;
  config: ITeachingBubblePropsExtended;
}