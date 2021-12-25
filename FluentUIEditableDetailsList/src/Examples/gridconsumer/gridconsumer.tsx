import { DirectionalHint, ThemeProvider, FontIcon, IButtonProps, Link, mergeStyles, mergeStyleSets, TeachingBubble, TextField } from '@fluentui/react';

import * as React from 'react';
import { useState } from 'react';
import EditableGrid from '../../libs/editablegrid/editablegrid';
import { GridColumnConfig, GridItemsType } from './gridconfig'
import { ITeachingBubbleConfig, ITeachingBubblePropsExtended, teachingBubbleConfig } from './teachingbubbleconfig';
import { useBoolean } from '@fluentui/react-hooks';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

import { initializeIcons } from '@fluentui/react';

initializeIcons(/* optional base url */);

const Consumer = () => {

  const [items, setItems] = useState<GridItemsType[]>([]);
  const [teachingBubbleVisible, { toggle: toggleTeachingBubbleVisible }] = useBoolean(true);
  const [teachingBubblePropsConfig, setTeachingBubblePropsConfig] = useState<ITeachingBubbleConfig>({ id: 0, config: {...teachingBubbleConfig[0], footerContent: `1 of ${teachingBubbleConfig.length}`}});

  const classNames = mergeStyleSets({
    controlWrapper: {
      display: 'flex',
      flexWrap: 'wrap',
    }
  });

  const iconClass = mergeStyles({
    fontSize: 20,
    margin: "0px 0px 0px 30px"
  });

  const onTeachingBubbleNavigation = (direction : string) => {
    switch (direction) {
      case 'previous':
        break;
      case 'next':
        var TeachingProps = teachingBubbleConfig[teachingBubblePropsConfig.id + 1];
        var currentId = teachingBubblePropsConfig.id + 1;
        TeachingProps.footerContent = `${currentId + 1} of ${teachingBubbleConfig.length}`;
        setTeachingBubblePropsConfig({ id: currentId, config: TeachingProps })
        break;
      case 'close':
        break;
    }
  }

  const nextBubbleProps: IButtonProps = {
    children: 'Next',
    onClick: () => onTeachingBubbleNavigation('next'),
  };

  const GetRandomInt = (min : number, max : number) : number => {
    min = Math.ceil(min);
    max = Math.floor(max);
    return Math.floor(Math.random() * (max - min + 1)) + min;
  };

  const SetDummyData = () : void => {
    var dummyData : GridItemsType[] = []
    for (var i = 1; i <= 100; i++){
      var randomInt = GetRandomInt(1,3);
      dummyData.push({
        id: i,
        name: 'Name' + GetRandomInt(1, 10),
        age: GetRandomInt(20,40)
      });
    }

    setItems(dummyData);
  }

  React.useEffect(() => {
    SetDummyData();
  }, []);

  return (
    <ThemeProvider>
      <ToastContainer />
      <div className={classNames.controlWrapper}>
        <TextField id="searchField" placeholder='Search Grid' className={mergeStyles({ width: '60vh', paddingBottom: '10px' })} />
        <Link>
          <FontIcon
            aria-label="View"
            iconName="View"
            className={iconClass}
            onClick={toggleTeachingBubbleVisible}
            id="tutorialinfo"
          />
        </Link>
      </div>

      {teachingBubbleVisible && (
        <TeachingBubble
          target={teachingBubblePropsConfig?.config.target}
          primaryButtonProps={teachingBubblePropsConfig?.id < teachingBubbleConfig.length - 1 ? nextBubbleProps : nextBubbleProps}
          secondaryButtonProps={nextBubbleProps}
          onDismiss={toggleTeachingBubbleVisible}
          footerContent={teachingBubblePropsConfig?.config.footerContent}
          headline={teachingBubblePropsConfig?.config.headline}
          hasCloseButton={true}
          isWide={teachingBubblePropsConfig?.config.isWide == null ? true : teachingBubblePropsConfig?.config.isWide}
          calloutProps={{
            directionalHint:DirectionalHint.bottomLeftEdge,
          }}
        >
          {teachingBubblePropsConfig?.config.innerText}
        </TeachingBubble>
      )}
    </ThemeProvider>
  )
}

export default Consumer;