import { Tutorial } from './Tutorial';
import { PageContext } from './PageContext';
import { UserActionProvider } from '../Models/User';

interface TokenProviderOptions {
  acquireToken: () => Promise<string>;
}

interface UserInfoOptions {
  getCurrentUser: () => string;
  getCurrentUserPermissions: () => string[];
}

interface NavigatorOptions {
  navigateToContext?: (context: PageContext) => void;

}

interface PageTourTheme {
  primaryColor: string;
  secondaryColor: string;
  textColor: string;
  navigationButtonColor: string;
  isRounded: boolean;
  borderRadius?: number;
  fontFamily?: string;
}

interface Tags {
  includedTags: string[];

  excludedTags: string[];
}

export interface PageTourOptions {
  tokenProvider?: TokenProviderOptions;
  userInfo?: UserInfoOptions;
  theme?: PageTourTheme;
  tourStartDelayInMs?: number;
  tags?: Tags;
  appInfo?: Map<string, string>;
  navigator?: NavigatorOptions;
  autoPlayDelay?: number;
  exportFeatureFlag?: boolean;
  autoPlayEnabled?: boolean;
  userActionProvider?: UserActionProvider;
  isCoverPageTourStart?:Boolean;
}
