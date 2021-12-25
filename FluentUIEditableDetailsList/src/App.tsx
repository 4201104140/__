import React from 'react';
import { Stack, Text, Link, FontWeights, IStackTokens, IStackStyles, ITextStyles } from '@fluentui/react';
import Consumer from './Examples/gridconsumer/gridconsumer';

export const App: React.FunctionComponent = () => {
  return (
    <Stack>
      <Consumer />
    </Stack>
  );
};