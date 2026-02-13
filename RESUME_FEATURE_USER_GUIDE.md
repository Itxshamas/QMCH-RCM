# Create Resume Feature - Visual Guide & Usage Instructions

## User Interface Layout

### 1. Applicant Form - Resume Section
```
???????????????????????????????????????
?     RESUME SECTION                  ?
???????????????????????????????????????
?                                     ?
?  ???????????????????????????????   ?
?  ? ?? Click to Upload Resume   ?   ?
?  ???????????????????????????????   ?
?                                     ?
?  ???????????????????????????????   ?
?  ? ?? Click to Create Resume   ?   ?
?  ?     (Turquoise Button)      ?   ?
?  ???????????????????????????????   ?
?                                     ?
???????????????????????????????????????
```

### 2. Resume Builder Page Structure

```
Navigation: Recruitment / Talent Acquisition / Candidate Management / Resume

???????????????????????????????????????????????????
? CANDIDATE RESUME BUILDER                ? Back  ?
???????????????????????????????????????????????????

SECTION 1: Personal Information
?? Mobile Name [text field]
?? Gender [dropdown: Male/Female/Other]
?? Date of Birth [date picker]

SECTION 2: Questions
?? Have you been convicted of a crime? [dropdown]
?  ?? (If Yes) Explain... [textarea]
?? What fears do you have about working with elderly? [textarea]
?? What's most challenging in this work? [textarea]

SECTION 3: Education [+Add Education]
?? [Table with rows]
?  ?? Degree [input]
?  ?? Institution [input]
?  ?? Major [input]
?  ?? Completion Date [input]
?  ?? Country [dropdown]
?  ?? City [input]
?  ?? [??? Delete]
?? [+ Add more rows]

SECTION 4: Skills [+Add Skill]
?? [Grid of skill items]
?  ?? Skill Name: [input]
?  ?? Level: [dropdown]
?  ?? [??? Delete]
?? [+ Add more rows]

SECTION 5: Employment History [+Add Employment]
?? [Table with rows]
?  ?? Company [input]
?  ?? From [input]
?  ?? To [input]
?  ?? Job Title [input]
?  ?? Location [input]
?  ?? Experience [input]
?  ?? May we contact? [dropdown]
?  ?? [??? Delete]
?? [+ Add more rows]

SECTION 6: Contact Information
?? Street Address [text field]
?? Country [dropdown]
?? State [text field]
?? City/Postal Code [text field]
?? Contact 1 [text field]
?? Contact 2 [text field]

SECTION 7: Availability
?? Hours per week [text field]
?? Available for night shift? [dropdown]
?? Available for weekends? [dropdown]
?? Available in other locations? [dropdown]
?? Preferred Employment Type [dropdown]
?? Available from Date [date picker]

SECTION 8: References [+Add Reference]
?? [Table with rows]
?  ?? Name [input]
?  ?? Relationship [input]
?  ?? Years Known [input]
?  ?? Phone [input]
?  ?? [??? Delete]
?? [+ Add more rows]

FORM ACTIONS
?? [? Cancel] [?? Save Resume]
```

## Step-by-Step Usage

### Creating a New Resume

**Step 1: Navigate to Applicant**
- Go to `/hr/aquisition/applicants/list`
- Click "New" button

**Step 2: Fill Applicant Information**
- Enter First Name (required)
- Enter Last Name (required)
- Enter Email
- Enter Phone
- Enter Position Applying For (required)
- Fill other optional fields

**Step 3: Create Resume**
- In Resume Section, click "Click to Create Resume" (turquoise button)
- This navigates to Resume Builder page

**Step 4: Fill Personal Information**
- Enter Middle Name (optional)
- Select Gender
- Enter Date of Birth

**Step 5: Answer Questions**
- Answer the three situational questions
- If "Yes" to criminal conviction, provide details

**Step 6: Add Education**
- Click "Add Education" button
- Fill in:
  - Degree (e.g., Bachelor's, Master's, Diploma)
  - Institution name
  - Major/Field of study
  - Completion Date
  - Country
  - City
- Click "Add Education" again to add more entries
- Delete unwanted entries with trash icon

**Step 7: Add Skills**
- Click "Add Skill" button
- Enter Skill Name
- Select Skill Level
- Click "Add Skill" for more entries
- Delete unwanted skills

**Step 8: Add Employment History**
- Click "Add Employment" button
- Fill in:
  - Company name
  - From date
  - To date
  - Job Title
  - Location
  - Brief experience description
  - May we contact? (Yes/No)
- Add more employment records as needed
- Delete unwanted records

**Step 9: Enter Contact Information**
- Enter Street Address
- Select Country
- Enter State/Province
- Enter City or Postal Code
- Enter up to 2 contact numbers

**Step 10: Set Availability**
- Enter hours per week available
- Select night shift availability
- Select weekend availability
- Select location flexibility
- Select employment type preference
- Pick available start date

**Step 11: Add References**
- Click "Add Reference" button
- Fill in:
  - Reference name
  - Relationship to candidate
  - Years known
  - Phone number
- Add more references as needed
- Delete unwanted references

**Step 12: Save Resume**
- Click "Save Resume" button
- System validates and saves all data
- Returns to Applicant form
- Resume is now linked to applicant

## Data Validation Rules

| Field | Required | Type | Max Length |
|-------|----------|------|-----------|
| Middle Name | No | Text | 100 |
| Gender | No | Dropdown | 50 |
| Date of Birth | No | Date | - |
| Criminal Conviction | No | Yes/No | 10 |
| Skills/Education/Employment | No | Dynamic | - |
| Street Address | No | Text | 200 |
| Country | No | Text | 100 |
| State | No | Text | 100 |
| Contact Fields | No | Text | 20 |

## Color Scheme

- **Header**: Turquoise (#17a2b8)
- **Section Dividers**: Light turquoise border
- **Buttons**:
  - Primary (Save): Turquoise background
  - Secondary (Add): Turquoise background
  - Delete: Red/Pink background
  - Cancel: Gray background
- **Hover Effects**: Darker shade + shadow

## Keyboard Shortcuts & Accessibility

- Tab key: Navigate between fields
- Enter: Submit form (on buttons)
- Arrow keys: Dropdown navigation
- Delete button: Remove entry (click or Tab+Enter)
- Back button: Return without saving

## Tips for Users

1. **Adding Multiple Entries**
   - Always click the "Add" button to create new rows
   - Empty rows are automatically removed when saving
   - At least one row per section is maintained

2. **Editing Existing Resume**
   - If resume exists, clicking Create Resume again loads existing data
   - You can update any field
   - All changes are preserved

3. **Data Organization**
   - Employment history should be in reverse chronological order (most recent first)
   - Skills should be ordered by importance/relevance
   - Education completed most recently should appear first

4. **Saving**
   - Always click "Save Resume" before navigating away
   - Clicking Cancel discards all unsaved changes
   - Back button returns to applicant form (assuming save was clicked)

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Form not saving | Check required fields, try again |
| Lost data | Unsaved changes are lost with Cancel, save first |
| Button not responding | Ensure form is not in an invalid state |
| Empty section persists | Must have at least one row; delete doesn't remove all |
| Can't navigate back | Click Back button or return link |

## Responsive Design

- **Desktop (1024px+)**: Full layout, side-by-side sections
- **Tablet (768px-1024px)**: Stacked layout, full-width inputs
- **Mobile (< 768px)**: Single column, touch-optimized buttons

## Features Highlight

? **Dynamic Sections** - Add or remove entries on the fly
?? **Data Validation** - Required fields are enforced
?? **Auto-Save Logic** - Creates or updates resume automatically
?? **Mobile Friendly** - Works on all devices
?? **Professional Design** - Clean, modern interface
? **Efficient Storage** - JSON format for complex data
?? **Create/Update** - Smart handling of new vs. existing resumes
?? **Complete Forms** - Comprehensive resume builder

## Support & Documentation

For more technical details, see: `RESUME_FEATURE_DOCUMENTATION.md`
