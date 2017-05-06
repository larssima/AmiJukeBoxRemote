import { inject, bindable, customElement, containerless, ObserverLocator } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import * as toastr from 'toastr';

@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog, ObserverLocator)

@containerless()

export class Welcome {
  heading = '';

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog, observerLocator) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.ea = eventAggregator;
    this.dateFrom = '';
    this.dateTo = '';
    this.observerLocator = observerLocator;
    this.enableImportBtn = true;
    this.observerLocator.getObserver(this, 'dateFrom').subscribe((newValue, oldValue) => this.enableImport());     
    this.observerLocator.getObserver(this, 'dateTo').subscribe((newValue, oldValue) => this.enableImport());     
  }
  
  activate() {
  }

  submit() {
    this.previousValue = this.fullName;
    alert(`Welcome, ${this.fullName}!`);
  }

  cancel()
  {
    return this.mapJukeboxService.cancelRecord();
  }

  canDeactivate() {
    /*
    if (this.fullName !== this.previousValue) {
      return confirm('Are you sure you want to leave?');
    }
    */
  }

  enableImport() {
    if(this.dateFrom!='' && this.dateFrom != null && this.dateTo!='' && this.dateTo != null)
      this.enableImportBtn = false;
    else
      this.enableImportBtn = true;
  }
}

export class UpperValueConverter {
  toView(value) {
    return value && value.toUpperCase();
  }
}
