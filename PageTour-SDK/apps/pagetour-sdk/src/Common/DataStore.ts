import { PageContext } from '../Models/PageContext';
import { Tutorial } from '../Models/Tutorial';
import { IPageTourRepository } from '../Repository/IPageTourRepository';
import { ConfigStore } from './ConfigStore';

export class DataStore {
  private isRepositoryInitialized: boolean;
  private repository: IPageTourRepository;
  private configStore: ConfigStore;

  constructor(repository: IPageTourRepository, configStore: ConfigStore) {
    this.repository = repository;
    this.configStore = configStore;
  }

  public GetTourById = async (tutorialId: string): Promise<Tutorial> => {
    let repository: IPageTourRepository = await this.GetRepository();
    let token = await this.acquireToken();
    return repository.GetTourById(tutorialId, token);
  }

  // public GetToursByPageContext = async (pageContext: any, getFutureAndExpired?: boolean): Promise<Tutorial[]> => {
  //   let repository: IPageTourRepository = await this.GetRepository();
    
  //   let contextParam = {} as PageContext;
  //   const opts = this.configStore.Options;
  //   if ((pageContext === undefined || pageContext === null) && opts.navigator && opts.navigator.getCurrentPageContext) {
  //     pageContext = opts.navigator.getCurrentPageContext();
  //     contextParam.state = pageContext.state;
  //     contextParam.url = pageContext.url;
  //   } else {
  //     if (typeof pageContext === 'string') {
  //       contextParam.state = pageContext;
  //       contextParam.url = pageContext;
  //     } else {
  //       if (pageContext.hasOwnProperty('state')) {
  //         contextParam.state = pageContext.state;
  //       }
  //       if (pageContext.hasOwnProperty('url')) {
  //         contextParam.url = pageContext.url;
  //       }
  //     }
  //   }

  //   return repository.GetToursByPageContext(searchFilter, token);
  // }

  public CreateTour = async (tutorial: Tutorial): Promise<Tutorial> => {
    let repository: IPageTourRepository = await this.GetRepository();
    let token = await this.acquireToken();
    return repository.CreateTour(tutorial, token);
  }

  public UpdateTour = async (tutorial: Tutorial): Promise<Tutorial> => {
    let repository: IPageTourRepository = await this.GetRepository()
    let token = await this.acquireToken();
    return repository.UpdateTour(tutorial, token);
  }

  public DeleteTour = async (tutorialId: string): Promise<Boolean> => {
    let repository: IPageTourRepository = await this.GetRepository()
    let token = await this.acquireToken()
    return repository.DeleteTour(tutorialId, token)
  }

  private GetRepository = async (): Promise<IPageTourRepository> => {
    if (this.isRepositoryInitialized === undefined || this.isRepositoryInitialized === false) {
      await this.repository.isInitialized().then((response: boolean) => {
        this.isRepositoryInitialized = response;
      })
    }
    if (this.isRepositoryInitialized === true) {
      return this.repository;
    } else {
      throw Error('Repository has not been initialized, use initialize repository method');
    }
  }

  private acquireToken = async (): Promise<string> => {
    return new Promise<string>((resolve, reject) => {
      const opts = this.configStore.Options;
      try {
        let token = opts.tokenProvider.acquireToken();
        resolve(token);
      } catch (err) {
        throw err;
      }
    })
  }
}