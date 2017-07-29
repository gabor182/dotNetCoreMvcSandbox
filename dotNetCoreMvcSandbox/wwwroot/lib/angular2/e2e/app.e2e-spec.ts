import { HeroesAppPage } from './app.po';

describe('heroes-app App', () => {
  let page: HeroesAppPage;

  beforeEach(() => {
    page = new HeroesAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
