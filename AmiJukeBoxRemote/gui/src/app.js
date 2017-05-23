export class App {
  configureRouter(config, router) {
    config.title = 'AMI Jukebox';
    config.map([
      { route: ['', 'selection'], name: 'selections',      moduleId: 'views/selection',      nav: true, title: 'Selections' },
      { route: 'archived', name: 'archived',      moduleId: 'views/selection-archived',      nav: true, title: 'Archive' },
      { route: 'admin', name: 'admin',  moduleId: 'views/admin',  nav: true, title: 'Administration' },
      { route: 'editsel', name: 'editsel',  moduleId: 'views/edit-selections',  nav: true, title: 'Edit Selections' },
      { route: 'editarch', name: 'editarch', moduleId: 'views/edit-archived-selections', nav: true, title: 'Edit Archived Selections'}
    ]);

    this.router = router;
  }
}
