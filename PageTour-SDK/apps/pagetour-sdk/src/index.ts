import { PageTourOptions } from './Models/PageTourOptions'
import { EventHandler } from './EventHandlers/EventHandler';
import { IPageTourRepository } from './Repository/IPageTourRepository';

class PageTour {
  private constructor() {
    this.eventHandlers = []
  }

  private eventHandlers: EventHandler[]

  public static init = (repository: IPageTourRepository, options: PageTourOptions): Promise<Boolean> => {

    return new Promise<Boolean>((resolve, reject) => {
      if (true) {
        resolve(true);
      }
      else{
        resolve(true);
      }
    })
  }
}

export { PageTour }