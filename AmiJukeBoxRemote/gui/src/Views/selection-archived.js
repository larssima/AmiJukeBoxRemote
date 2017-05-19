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

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog,observerLocator) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.mapJukeboxSelections = [];
    this.ea = eventAggregator;
    this.dateFrom = '';
    this.dateTo = '';
    this.observerLocator = observerLocator;
    this.enableImportBtn = true;
    this.selectedA = '';
    this.selectedB = ''; 
    this.artist1 = '';
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
    this.selectedA = jbselection.A1Song;
    this.selectedB = jbselection.B1Song;
    this.artist1 = jbselection.Artist1;
     $('#play-spotify-modal').modal('show');
  }  
  
  playSongOnSpotify(artist,song,qued)
  {
    return this.mapJukeboxService.playSongOnSpotify(artist,song,qued).then(data => {
      $('#play-spotify-modal').modal('hide');
    })
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
