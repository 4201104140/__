import * as tourBoxHtml from './tour-box.html';
import { State, Step, Tutorial } from '../Models/Tutorial';

import { DataStore } from '../Common/DataStore';

export class PageTourPlay {
  private tour: any = JSON.parse('{}');

  private autoPlayTest: boolean;

  public isTourPlaying: boolean;
  
  public playTour = async (tourId: any, startInterval: any , autoPlayTest: boolean = false) => {
    try {
      this.autoPlayTest = autoPlayTest;
      this.runTour(this.tour, startInterval, null, autoPlayTest);
    } catch (err) {
      throw new Error(err as string);
    }
  }

  public runTour = (
    objTour: Tutorial,
    startInterval: any,
    callback: any = null,
    autoPlayTest: boolean = false,
  ) => {
    if (this.isTourPlaying) {
      return;
    }
    this.isTourPlaying = true;
    this.tour = objTour;
    if (objTour.steps[0].delayBefore) {
      startInterval = parseInt(String(objTour.steps[0].delayBefore), 10) * 1000;
    }
    if (objTour.coverPage && objTour.coverPage.location.toLowerCase() === 'start') {
      console.log('hi');
    } else {
      console.log('hi');
    }
  }
}