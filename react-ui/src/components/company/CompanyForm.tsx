// components/CompanyForm.tsx
import React, { useEffect, useState } from 'react';
import { Box, Button, TextField, Grid } from '@mui/material';
import type { Company } from '../../types/company';

// interface Company {
//   id?: number;
//   name: string;
//   zipCode: string;
//   address: string;
//   phoneNumber: string;
//   emailAddress: string;
//   homepageUrl?: string;
//   establishedDate: string;
//   remarks?: string;
// }

interface Props {
  initialData?: Company;
  onSubmit: (company: Company) => void;
  onCancel?: () => void;
}

const CompanyForm: React.FC<Props> = ({ initialData, onSubmit, onCancel }) => {
  const [form, setForm] = useState<Company>({
    id: undefined,
    name: '',
    zipCode: '',
    address: '',
    phoneNumber: '',
    emailAddress: '',
    homepageUrl: '',
    establishedDate: new Date().toISOString().split('T')[0],
    remarks: '',
    ...initialData,
  });

   useEffect(() => {
    if (initialData) {
      setForm({
        ...initialData,
        establishedDate: new Date(initialData.establishedDate).toISOString().split('T')[0],
      });
    }else {
      // Clear form when no initialData
      setForm({
        id: undefined,
        name: '',
        zipCode: '',
        address: '',
        phoneNumber: '',
        emailAddress: '',
        homepageUrl: '',
        establishedDate: new Date().toISOString().split('T')[0],
        remarks: '',
      });
    }
  }, [initialData]);
  
  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit(form);
  };

  return (
    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 2 }}>
      <Grid container spacing={2}>
        <Grid size={{ xs: 12, md: 6 }}>
          <TextField fullWidth required label="Name" name="name" value={form.name} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <TextField fullWidth required label="Zip Code" name="zipCode" value={form.zipCode || ''} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12, sm:3}}>
          <TextField fullWidth required label="Address" name="address" value={form.address || ''} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <TextField fullWidth required label="Phone" name="phoneNumber" value={form.phoneNumber || ''} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12, sm: 3 }}>
          <TextField fullWidth required label="Email" name="emailAddress" value={form.emailAddress || ''} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12,sm: 3  }}>
          <TextField fullWidth label="Homepage" name="homepageUrl" value={form.homepageUrl || ''} onChange={handleChange} />
        </Grid>
        <Grid size={{ xs: 12,sm: 3  }}>
          <TextField fullWidth required type="date" label="Established Date" name="establishedDate" value={form.establishedDate} onChange={handleChange} InputLabelProps={{ shrink: true }} />
        </Grid>
        <Grid size={{ xs: 12 }}>
          <TextField fullWidth multiline rows={3} label="Remarks" name="remarks" value={form.remarks || ''} onChange={handleChange} />
        </Grid>
      </Grid>

      <Box mt={2}>
        <Button type="submit" variant="contained" color="primary">
          {form.id ? 'Update' : 'Create'}
        </Button>
        {onCancel && (
          <Button
  type="button"  
  sx={{ ml: 2 }}
  onClick={onCancel}
  variant="outlined"
>
  Cancel
</Button>

        )}
      </Box>
    </Box>
  );
};

export default CompanyForm;
