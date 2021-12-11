import { PageTourOptions } from '../Models/Options'
import { PageContext } from '../Models/PageContext'

export class ConfigStore {
  private options: PageTourOptions;
  private defaultOptions: PageTourOptions = {
    autoPlayTest: 5000,

  };
  constructor(options: PageTourOptions) {
    this.extendOptions(options);
  }
  public get Options(): PageTourOptions {
    return this.options;
  }
  private extendOptions = (inputOptions: PageTourOptions): PageTourOptions => {
    return (this.options = { ...this.defaultOptions, ...inputOptions });
  }
}