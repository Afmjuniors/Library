import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { NewChecklistForm } from './newChecklistForm.component';



describe('NewChecklistForm', () => {
  let component: NewChecklistForm;
  let fixture: ComponentFixture<NewChecklistForm>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ NewChecklistForm ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(NewChecklistForm);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
