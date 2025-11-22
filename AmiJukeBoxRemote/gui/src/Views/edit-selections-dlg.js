import {inject} from 'aurelia-framework';
import {DialogController} from 'aurelia-dialog';
import {HttpClient} from 'aurelia-fetch-client';
import {DiscogsConfig} from '../Services/discogs-config';

@inject(DialogController, HttpClient)
export class EditJbSelection {
    jbselection = {};
    title = '';
    discogsSearchQuery = '';
    discogsResults = [];
    selectedDiscogsRelease = null;
    isSearching = false;

  constructor(dialogController, httpClient){
    this.controller = dialogController;
    this.httpClient = httpClient;
  }
  
  activate(data){
      this.jbselection = data.jbselection;
      this.title = data.title;
  }

  async searchDiscogs() {
    if (!this.discogsSearchQuery || this.discogsSearchQuery.trim() === '') {
      return;
    }

    this.isSearching = true;
    this.discogsResults = [];
    
    try {
      const query = encodeURIComponent(this.discogsSearchQuery);
      const headers = {
        'User-Agent': DiscogsConfig.userAgent
      };
      
      // Add authorization if token is configured
      if (DiscogsConfig.token && DiscogsConfig.token !== 'YOUR_DISCOGS_TOKEN_HERE') {
        headers['Authorization'] = `Discogs token=${DiscogsConfig.token}`;
      }
      
      const response = await this.httpClient.fetch(
        `https://api.discogs.com/database/search?q=${query}&type=release&format=vinyl&per_page=20`,
        { headers }
      );
      
      const data = await response.json();
      this.discogsResults = data.results || [];
    } catch (error) {
      console.error('Discogs search error:', error);
      alert('Error searching Discogs. Please check your connection and API token.');
    } finally {
      this.isSearching = false;
    }
  }

  async populateFromDiscogs() {
    if (!this.selectedDiscogsRelease) {
      return;
    }

    try {
      const headers = {
        'User-Agent': DiscogsConfig.userAgent
      };
      
      // Add authorization if token is configured
      if (DiscogsConfig.token && DiscogsConfig.token !== 'YOUR_DISCOGS_TOKEN_HERE') {
        headers['Authorization'] = `Discogs token=${DiscogsConfig.token}`;
      }
      
      // Fetch detailed release information
      const response = await this.httpClient.fetch(
        this.selectedDiscogsRelease.resource_url,
        { headers }
      );
      
      const release = await response.json();
      
      // Populate artist
      if (release.artists && release.artists.length > 0) {
        this.jbselection.Artist1 = release.artists[0].name;
        if (release.artists.length > 1) {
          this.jbselection.Artist2 = release.artists[1].name;
        }
      }

      // Populate songs from tracklist
      if (release.tracklist && release.tracklist.length > 0) {
        const aSideTracks = release.tracklist.filter(t => t.position && t.position.startsWith('A'));
        const bSideTracks = release.tracklist.filter(t => t.position && t.position.startsWith('B'));
        
        if (aSideTracks.length > 0) {
          this.jbselection.A1Song = aSideTracks[0].title;
          if (aSideTracks.length > 1) {
            this.jbselection.A2Song = aSideTracks[1].title;
          }
        }
        
        if (bSideTracks.length > 0) {
          this.jbselection.B1Song = bSideTracks[0].title;
          if (bSideTracks.length > 1) {
            this.jbselection.B2Song = bSideTracks[1].title;
          }
        }
      }

      // Populate Discogs link
      if (release.uri) {
        this.jbselection.DiscogsLink = `https://www.discogs.com${release.uri}`;
      }

      // Populate music category from genres
      if (release.genres && release.genres.length > 0) {
        this.jbselection.MusicCategory = release.genres.join(', ');
      }

    } catch (error) {
      console.error('Error fetching release details:', error);
      alert('Error loading release details from Discogs.');
    }
  }
}