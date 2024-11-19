import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { NewOccurrenceRecordComponent } from './new-occurrence-record.component';

describe('NewOccurrenceRecordComponent', () => {
  let component: NewOccurrenceRecordComponent;
  let fixture: ComponentFixture<NewOccurrenceRecordComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewOccurrenceRecordComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewOccurrenceRecordComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
