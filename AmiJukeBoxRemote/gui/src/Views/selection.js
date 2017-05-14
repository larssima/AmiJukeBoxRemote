import { inject, bindable, customElement, containerless, ObserverLocator } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import { ChooseSideDialog } from '../Components/chooseside-dialog';
import * as toastr from 'toastr';

@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog, ChooseSideDialog, ObserverLocator)

@containerless()

export class Welcome {
  heading = '';

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog, chooseSideDialog, observerLocator) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.mapJukeboxSelections = [];
    this.choosemessage = "Test";
    this.ea = eventAggregator;
    this.dateFrom = '';
    this.dateTo = '';
    this.observerLocator = observerLocator;
    this.enableImportBtn = true;
    this.observerLocator.getObserver(this, 'dateFrom').subscribe((newValue, oldValue) => this.enableImport());     
    this.observerLocator.getObserver(this, 'dateTo').subscribe((newValue, oldValue) => this.enableImport());     
  }
  


  handleHold($event) {
    var _this = this;
    this.dialogService.open({ viewModel: ConfirmDialog, model: { value: "Are you sure you want to cancel record?" } }).then(function (response) {
      if (!response.wasCancelled) {
        _this.Cancel().then((activity) => {
          _this.loadData()
        });
      }
    });
  }

  handlePress($event,jbselection) {
    var _this = this;
    this.choosemessage = "A: " + jbselection.A1Song + " - " + "B: " + jbselection.B1Song;
    this.dialogService.open({ viewModel: ChooseSideDialog, model: { value: this.choosemessage } }).then(function (response) {
      if (!response.wasCancelled) {
        // Play Side A
      }
      else {
        // Play Side B
      }
    });
  }  

  attached() {
    return this.loadData();
  }

  loadData() {
    return this.mapJukeboxService.getAllJukeboxSelections().then(data => {
      this.mapJukeboxSelections = data;
      });
  }

  getImagePath(jbselection){
    return "../../assets/images/" + jbselection.ImageStripName;
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

  createstrips()
  {
    var _this = this;
    return this.mapJukeboxService.createStrips().then(data => {
      _this.loadData();
    });
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
