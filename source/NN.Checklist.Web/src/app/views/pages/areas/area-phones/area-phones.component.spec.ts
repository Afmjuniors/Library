import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AreaPhonesComponent } from './area-phones.component';

describe('AreaPhonesComponent', () => {
  let component: AreaPhonesComponent;
  let fixture: ComponentFixture<AreaPhonesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AreaPhonesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AreaPhonesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
