import React from 'react';
import { Stack, Text, Link, FontWeights, IStackTokens, IStackStyles, ITextStyles } from '@fluentui/react';

const containerStackTokens: IStackTokens = {
	childrenGap: 5,
	padding: 10,
}

export const TestStack : React.FunctionComponent = () => {
	return (
		<Stack tokens={containerStackTokens}>
			<span>Default</span>
		</Stack>
	);
};