import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CatFactHomeComponent } from './catFact-home.component';

describe('CatFactHomeComponent', () => {
  let component: CatFactHomeComponent;
  let fixture: ComponentFixture<CatFactHomeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CatFactHomeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CatFactHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
