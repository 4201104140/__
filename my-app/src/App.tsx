import React from 'react';
import { Stack, Text, Link, FontWeights, IStackTokens, IStackStyles, ITextStyles, DefaultPalette, IStackItemStyles } from '@fluentui/react';
import logo from './logo.svg';
import { TestStack } from './ui/Stack';
import { Navbar } from './ui/Navbar';
import './App.css';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';

const boldStyle: Partial<ITextStyles> = { root: { fontWeight: FontWeights.semibold } };
const stackStyles: Partial<IStackStyles> = {
  root: {
    background: DefaultPalette.themeTertiary,
  },
};

const stackItemStyles: IStackItemStyles = {
  root: {
    alignItems: 'center',
    background: DefaultPalette.themePrimary,
    color: DefaultPalette.white,
    display: 'flex',
    height: 50,
    justifyContent: 'center',
  },
};

const stackTokens: IStackTokens = {
  childrenGap: 5,
  padding: 10,
}

export const App: React.FunctionComponent = () => {
  return (
    <Router>
      <Navbar ariaLabel=''/>
      <Switch>
        <Route path="/examples/activityitem">
          
        </Route>
        <Route path="/test2">
          <TestStack />
        </Route>
      </Switch>
    </Router>
  )
}
