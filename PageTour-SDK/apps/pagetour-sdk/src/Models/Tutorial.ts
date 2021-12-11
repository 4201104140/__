export interface State {
  name: string;

  params: any[];

  requiredLogin: Boolean;

  templateUrl: string;

  url: string;

  type: string;
}

export interface Step {
  key: string

  delayBefore?: number

  selector: string

  silent: Boolean

  pageContext: string

  pageState: State

  pageStateName: string

  type: string

  position: string

  message: string

  errorMessage: string

  value: string

  executionLoad: Boolean

  ignoreStepIf: Boolean

  ignoreStepIfConditions: string
}

export interface CoverPage {
  location: string

  content: string
}

export interface BaseEntity {
  createdOn: Date;

  lastModifiedOn: Date;

  createdBy: string;

  lastModifiedBy: string;
}

export interface Tutorial extends BaseEntity {
  id: string;

  description: string;

  tutorialId: string;

  title: string;

  pageContext: string;

  coverPage: CoverPage;

  applicationName: string;

  steps: Step[];

  startPageUrl: string;

  pageStates: State[];

  expireOn: Date;

  isAutoPlayEnabled: boolean;

  activeOn: Date;

  isActive: boolean;

  isDeleted: boolean;

  version: number;

  tags: string[];

  createdBy: string;

  lastModifiedBy: string;
}

export interface TutorialSearchFilter {
  active: boolean

  pageContextState: string

  pageContextUrl: string

  includeTags: string[]

  excludeTags: string[]
}