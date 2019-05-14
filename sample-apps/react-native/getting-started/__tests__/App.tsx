import * as React from 'react';
import App from '../src/App';
import * as renderer from 'react-test-renderer';
jest.mock('react-native-code-push', () => 'CodePush');
jest.mock('appcenter-crashes', () => 'AppCenterCrashes');

it('renders correctly', () => {
  const tree = renderer.create(<App />);
});
