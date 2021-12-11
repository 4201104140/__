export interface UserActions {
  id: string;

  tutorialId: string;

  version: number;

  user: string;

  startedOn: Date;

  completedOn: Date;
}

export interface UserActionProvider {
  recordUserAction: (tutorial: any, userAction: string, step: string, operation: string) => Promise<UserActions>
}

export enum UserPermissionsEnum {
  get = 'get',
  list = 'list',
  update = 'update',
  create = 'create',
  delete = 'delete',
  import = 'import',
  export = 'export',
}