# Discogs Integration Guide

## Overview
The edit selections dialog now includes Discogs catalog search functionality. You can search your Discogs catalog and automatically populate artist names and song titles from vinyl 7" records.

## Setup

### 1. Get a Discogs API Token
1. Go to https://www.discogs.com/settings/developers
2. Log in to your Discogs account
3. Generate a new personal access token
4. Copy the token

### 2. Configure the Token
Open `src/Services/discogs-config.js` and replace `YOUR_DISCOGS_TOKEN_HERE` with your actual token:

```javascript
export const DiscogsConfig = {
  token: 'your_actual_token_here',
  userAgent: 'JukeboxApp/1.0 +http://yourapp.com'
};
```

## Usage

### Searching Discogs
1. Open the edit selections dialog
2. In the "Search Discogs Catalog" section at the top, enter an artist name or release title
3. Click the "Search" button
4. Results will appear in the dropdown below (filtered for vinyl releases)

### Auto-Populating Fields
1. Select a release from the search results dropdown
2. The following fields will be automatically populated:
   - **Artist1**: Primary artist name
   - **Artist2**: Secondary artist (if available)
   - **A1Song**: First track on A-side
   - **A2Song**: Second track on A-side (if available)
   - **B1Song**: First track on B-side
   - **B2Song**: Second track on B-side (if available)
   - **DiscogsLink**: Direct link to the release on Discogs
   - **MusicCategory**: Genre information

### Manual Editing
After auto-populating, you can still manually edit any field before saving.

## Features
- Searches specifically for vinyl releases
- Displays release year and format in results
- Automatically extracts A-side and B-side tracks
- Handles multiple artists
- Populates genre/category information
- Creates direct Discogs links

## API Rate Limits
Discogs API has rate limits:
- **Without authentication**: 25 requests per minute
- **With authentication**: 60 requests per minute

Using a personal access token (as configured) gives you the higher rate limit.

## Troubleshooting

### "Error searching Discogs"
- Check your internet connection
- Verify your API token is correct in `discogs-config.js`
- Check browser console for detailed error messages

### No results found
- Try different search terms
- The search is filtered for vinyl releases only
- Some releases may not be in the Discogs database

### Missing track information
- Some releases may not have complete tracklist data
- Manually enter missing information after auto-population
