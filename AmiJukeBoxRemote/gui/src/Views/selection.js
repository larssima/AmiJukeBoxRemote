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
    this.mapJukeboxSelections = [];
    this.choosemessage = "Test";
    this.ea = eventAggregator;
    this.dateFrom = '';
    this.dateTo = '';
    this.observerLocator = observerLocator;
    this.enableImportBtn = true;
    this.observerLocator.getObserver(this, 'dateFrom').subscribe((newValue, oldValue) => this.enableImport());     
    this.observerLocator.getObserver(this, 'dateTo').subscribe((newValue, oldValue) => this.enableImport());     
    this.selectedA = "";
    this.selectedB = "";
    this.jbLetter = "";
    this.jbNumberA = "";
    this.jbNumberB = "";
  }
  


  handleHold($event,jbselection) {
    var _this = this;
    this.jbLetter = jbselection.JbLetter;
    this.jbNumberA = jbselection.JbNumberA;
    $('#admin-jukebox-modal').modal('show');
    // this.dialogService.open({ viewModel: ConfirmDialog, model: { value: "Are you sure you want to cancel record?" } }).then(function (response) {
    //   if (!response.wasCancelled) {
    //     _this.Cancel().then((activity) => {
    //       _this.loadData()
    //     });
    //   }
    // });
  }

  isOdd(num) { return num % 2;}

  randomsong()
  {
    var count = this.mapJukeboxSelections.length*2;
    var random = Math.floor((Math.random() * count) + 1);
    var aside = false;
    if(this.isOdd(random)){
      aside = true;
      random = random + 1;
    }
    var recordnr = random/2;
    var sel = this.mapJukeboxSelections[recordnr-1];                                                           
    if(aside)
    {
       toastr.success(sel.A1Song+" ["+sel.JbLetter+sel.JbNumberA+"] selected!");
    }
    else
    {
       toastr.success(sel.B1Song+" ["+sel.JbLetter+sel.JbNumberB+"] selected!");
    }
  }

  openPlaySongDlg($event,jbselection) {
    
    if($event.offsetY<=50)
    {
       this.playSongOnJukebox(jbselection.JbLetter,jbselection.JbNumberA);
       toastr.success(jbselection.A1Song+" ["+jbselection.JbLetter+jbselection.JbNumberA+"] selected!");
    }
    if($event.offsetY>50)
    {
       this.playSongOnJukebox(jbselection.JbLetter,jbselection.JbNumberB);
       toastr.success(jbselection.B1Song+" ["+jbselection.JbLetter+jbselection.JbNumberB+"] selected!");
    }
    this.jbLetter = jbselection.JbLetter;
    this.jbNumberA = jbselection.JbNumberA;
    this.jbNumberB = jbselection.JbNumberB;
    this.selectedA = jbselection.A1Song;
    this.selectedB = jbselection.B1Song;    
    // $('#play-jukebox-modal').modal('show');
  }

  getMapName(jbselection,num)
  {
    if(num==1) return "map" + jbselection.Id + "A";
    if(num==2) return "map" + jbselection.Id + "B";
  }

  getUseMapName(jbselection)
  {
    return "map" + jbselection.Id + "A";
  }

  handlePress($event,jbselection) {
    this.jbLetter = jbselection.JbLetter;
    this.jbNumberA = jbselection.JbNumberA;
    this.jbNumberB = jbselection.JbNumberB;
    this.selectedA = jbselection.A1Song;
    this.selectedB = jbselection.B1Song;    
    $('#play-jukebox-modal').modal('show');
  }  

  playSongOnJukebox(jbLetter,jbNumber)
  {
    return this.mapJukeboxService.playSongOnJukebox(jbLetter,jbNumber).then(data => {
      $('#play-jukebox-modal').modal('hide');
    })
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
