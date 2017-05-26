import {inject} from 'aurelia-framework';
import {AppSettings} from '../infrastructure/app-settings';
import {BaseRepo} from '../infrastructure/baserepo';

@inject(AppSettings, BaseRepo)
export class MapJukeboxService {
    constructor(appSettings, baseRepo) {
        this.baseRepo = baseRepo;
    }

    cancelRecord(){
        return this.baseRepo.get('amijukebox/cancel').then(data=> {return data;});
    }

    createStrips(){
        return this.baseRepo.get('amijukebox/createstrips').then(data=> {return data;});
    }

    getAllJukeboxSelections(){
        return this.baseRepo.get('amijukebox/getalljukeboxselections').then(data=> {return data;});
    }

    getAllArchivedJukeboxSelections(){
        return this.baseRepo.get('amijukebox/getallarchivedjukeboxselections').then(data=> {return data;});
    }

    loginToSpotify()
    {
        return this.baseRepo.get('amijukebox/spotifylogin').then(data=> {return data;});
    }

    reinstateSelection(jbsel,jbletter,jbnumbera)
    {
        var numba = parseInt(jbnumbera);
        if(jbnumbera<1 || jbnumbera>19 || jbsel.Id<0) return
        var recordnr = -1
        if(jbletter=='A') recordnr=0
        if(jbletter=='B') recordnr=10
        if(jbletter=='C') recordnr=20
        if(jbletter=='D') recordnr=30
        if(jbletter=='E') recordnr=40
        if(jbletter=='F') recordnr=50
        if(jbletter=='G') recordnr=60
        if(jbletter=='H') recordnr=70
        if(jbletter=='J') recordnr=80
        if(jbletter=='K') recordnr=90
        if(recordnr==-1 || !this.isOdd(numba)) return
        var num = (numba + 1) / 2
        recordnr = recordnr + num
        let indata = {
            Id: jbsel.Id,
            JbLetter: jbletter,
            JbNumberA: jbnumbera,
            JbNumberB: String(numba+1),
            JbNumeric: recordnr,
            A1Song: jbsel.A1Song,
            A2Song: jbsel.A2Song,
            B1Song: jbsel.B1Song,
            B2Song: jbsel.B2Song,
            Artist1: jbsel.Artist1,
            Artist2: jbsel.Artist2,
            ImageStripTemplate: jbsel.ImageStripTemplate,
            MusicCategory: jbsel.MusicCategory,
            Archived: 0
        };        
        return this.baseRepo.put('amijukebox/reinstateselection',indata).then(data=> {return data;});
    }

    isOdd(num) { return num % 2;}

    archiveSelection(id)
    {
        let indata = {
            Id: id
        }
        return this.baseRepo.put('amijukebox/archiveselection',indata).then(data => {return data;});
    }

    playSongOnSpotify(artist,songtitle,que)
    {
        let data = {
            Artist: artist,
            SongTitle: songtitle,
            Que: que
        };          
        return this.baseRepo.put('amijukebox/playsongonspotify',data)
    }

    playSongOnJukebox(jbLetter,jbNumber)
    {
        let data = {
            JbLetter: jbLetter,
            JbNumber: jbNumber
        }
        return this.baseRepo.get('amijukebox/playsongonjukebox',data)
    }

    insertSelection(jbletter,jbnumberA,jbnumberB,jbnumeric,a1song,a2song,b1song,b2song,artist1,artist2,imagestriptemplate,musiccategory,archived){
        let data = {
            JbLetter: jbletter,
            JbNumberA: jbnumberA,
            JbNumberB: jbnumberB,
            JbNumeric: jbnumeric,
            A1Song: a1song,
            A2Song: a2song,
            B1Song: b1song,
            B2Song: b2song,
            Artist1: artist1,
            Artist2: artist2,
            ImageStripTemplate: imagestriptemplate,
            MusicCategory: musiccategory,
            Archived: archived
        };        
        return this.baseRepo.put('amijukebox/savestrip', data)
    }    
}