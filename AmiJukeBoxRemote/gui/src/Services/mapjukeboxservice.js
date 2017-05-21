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