import { inject, bindable, customElement, containerless, ObserverLocator } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import { ChooseSideDialog2 } from '../Components/chooseside-dialog2';
import * as toastr from 'toastr';

@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog, ChooseSideDialog2, ObserverLocator)

@containerless()

export class Welcome {
  heading = '';

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog, chooseSideDialog2, observerLocator) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.mapJukeboxSelections = [];
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
    this.dialogService.open({ viewModel: ChooseSideDialog2, model: { sideA: jbselection.A1Song, sideB: jbselection.B1Song } }).then(function (response) {
      if (!response.wasCancelled) {
        this.mapJukeboxService.playSongOnSpotify(jbselection.Artist1,jbselection.A1Song,1);
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
    return this.mapJukeboxService.getAllArchivedJukeboxSelections().then(data => {
      this.mapJukeboxSelections = data;
      });
  }

  getImagePath(jbselection){
    return "../../assets/images/" + jbselection.ImageStripName;
  }

  activate() {
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
}

export class UpperValueConverter {
  toView(value) {
    return value && value.toUpperCase();
  }
}
