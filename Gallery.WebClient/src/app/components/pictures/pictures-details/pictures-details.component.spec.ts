import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PicturesDetailsComponent } from './pictures-details.component';

describe('PicturesDetailsComponent', () => {
  let component: PicturesDetailsComponent;
  let fixture: ComponentFixture<PicturesDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PicturesDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PicturesDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
