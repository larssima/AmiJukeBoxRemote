import { inject, bindable, customElement, containerless, ObserverLocator } from 'aurelia-framework';
import { EventAggregator } from 'aurelia-event-aggregator';
import { MapJukeboxService } from '../services/mapjukeboxservice';
import { DialogService } from 'aurelia-dialog';
import { ConfirmDialog } from '../Components/confirmation-dialog';
import { SelectionInfo } from './selections-info-dlg';
import { FilterValueConverter } from '../Components/filter-value-converter';
import * as toastr from 'toastr';

@inject(MapJukeboxService, EventAggregator, DialogService, ConfirmDialog, ObserverLocator)

@containerless()

export class Welcome {
  heading = '';

  constructor(mapJukeboxService, eventAggregator, dialogService, confirmDialog, observerLocator) {
    this.mapJukeboxService = mapJukeboxService;
    this.dialogService = dialogService;
    this.mapJukeboxSelections = [];
    this.mapJukeboxPrintSel = [];
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
    this.randaside = true;
    this.randbside = true;
    this.selectedjb = "";
  }
  
  filterFunc(searchExpression, value){
     
     let itemValue = value.A1Song+" "+value.B1Song+" "+value.Artist1+" "+value.Artist2;
     if(!searchExpression || !itemValue) return false;
     
     return itemValue.toUpperCase().indexOf(searchExpression.toUpperCase()) !== -1;
     
  }

  printdlg()
  {
    this.mapJukeboxPrintSel = 
    $('#play-jukebox-modal').modal('show');
  }

  showSelectionInfo(jbselectionInfo) {
        var infoSelection = Object.assign({}, jbselectionInfo);

        this.dialogService.open({viewModel: SelectionInfo, model: { jbselection: infoSelection, title: 'Selection info' } }).then(response => {
                return response.wasCancelled;
            });
  }


  handleHold($event,jbselection) {
    //var _this = this;
    this.selectedjb = jbselection;
    //jbselection.SelectedForPrint = true;
    //return $('#admin-jukebox-modal').modal('show');
    this.showSelectionInfo(this.selectedjb).then(response => {
        return response.wasCancelled;
    });
    // this.dialogService.open({ viewModel: ConfirmDialog, model: { value: "Are you sure you want to cancel record?" } }).then(function (response) {
    //   if (!response.wasCancelled) {
    //     _this.Cancel().then((activity) => {
    //       _this.loadData()
    //     });
    //   }
    // });
  }
  
  shuffle(array) {
    var tmp, current, top = array.length;
    if(top) while(--top) {
      current = Math.floor(Math.random() * (top + 1));
      tmp = array[current];
      array[current] = array[top];
      array[top] = tmp;
    }
    return array;
  }

  myLoop(nrofsongs) {    
    var rnr = 0;      
    var arr1 = Array(200).fill(0).map((e,i)=>i+1);
    console.log("Arr1: " +arr1);
    var arr2 = this.shuffle(arr1);
    console.log("Arr2: " + arr2);
    for (let i=1; i<=nrofsongs; i++) {
      setTimeout( function timer(){
          console.log("Song: " + i + " randomnr: " + (arr2[i-1]));
      }, i*3000 );
    }  
  };   

  randomsong10() {
    this.myLoop(1);
  }

  isOdd(num) { return num % 2;}

  randomnr()
  {
    var count = this.mapJukeboxSelections.length*2;
    var random = Math.floor((Math.random() * count) + 1);
    return random;
  }

  randomsong()
  {
    if (this.randaside==false && this.randbside==false) exit;
    var random = this.randomnr();
    var aside = false;
    if(this.isOdd(random)){
      aside = true;
      random = random + 1;
    }
    var recordnr = random/2;
    var sel = this.mapJukeboxSelections[recordnr-1];                                                           
    if(this.randaside && !this.randbside)
    {
        aside=true;
    }
    if(this.randbside && !this.randaside)
    {
       aside=false;
    }
    if(aside)
    {
       this.playSongOnJukebox(sel.JbLetter,sel.JbNumberA);
       toastr.success(sel.A1Song+" ["+sel.JbLetter+sel.JbNumberA+"] selected!");
    }
    else
    {
       this.playSongOnJukebox(sel.JbLetter,sel.JbNumberB);
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
