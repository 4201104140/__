import * as allToursModal from './all-tours-modal.html';
import { DomUtils } from '../Common/DomUtils';

export class PageTourManager {

  private allToursModalFn: any = allToursModal;

  /// Initializes Manage Tour Dialog
  public InitManagerDock = async () => {
    await this.openTourMangerDialog();
  }

  /// Opens Manage Tours Dialog
  private openTourMangerDialog = async () => {
    let manageTourModal = document.getElementById('manage-tours-modal');
    if (!manageTourModal) {
      let manageTourBox = this.allToursModalFn();
      manageTourBox = DomUtils.appendToBody(manageTourBox);
      DomUtils.show(manageTourBox);
    } else {
      manageTourModal.style.display = 'block';
    }

    
  }
}