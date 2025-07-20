// pages/CompanyPage.tsx
import { useState } from 'react';
import { Box, Typography, Divider, Snackbar, Alert, Dialog, Button, DialogActions, DialogContent, DialogContentText, DialogTitle } from '@mui/material';
import { useQuery, useMutation } from '@apollo/client';
import { CREATE_COMPANY, DELETE_COMPANY, GET_COMPANIES, UPDATE_COMPANY } from '../../graphql/companyQueries';
import CompanyForm from '../../components/company/CompanyForm';
import CompanyTable from '../../components/company/CompanyTable';
import type { Company } from '../../types/company';

interface GetAllCompaniesData {
  allCompanies: Company[];
}

const CompanyPage = () => {
  const { data, loading, error, refetch } = useQuery<GetAllCompaniesData>(GET_COMPANIES);
  const [createCompany] = useMutation(CREATE_COMPANY);
  const [updateCompany] = useMutation(UPDATE_COMPANY);
  const [deleteCompany] = useMutation(DELETE_COMPANY);
const [deleteDialogOpen, setDeleteDialogOpen] = useState(false);
const [companyToDelete, setCompanyToDelete] = useState<number | null>(null);

  const [selectedCompany, setSelectedCompany] = useState<Company | null>(null);


  const [snackbar, setSnackbar] = useState<{ open: boolean; message: string; severity: 'success' | 'error' }>({
    open: false,
    message: '',
    severity: 'success',
  });

  const showSnackbar = (message: string, severity: 'success' | 'error' = 'success') => {
    setSnackbar({ open: true, message, severity });
  };

 const handleSubmit = async (company: Company) => {
    try {
      const payload = {
        ...company,
        establishedDate: new Date(company.establishedDate).toISOString(),
      };

      if (company.id) {
        await updateCompany({ variables: payload });
        showSnackbar('Company updated successfully', 'success');
      } else {
        await createCompany({ variables: payload });
        showSnackbar('Company created successfully', 'success');
      }

      setSelectedCompany(null);
      refetch();
    } catch (error) {
      showSnackbar('Failed to save company', 'error');
    }
  };

  // const handleDelete = async (id: number) => {
  //   try {
  //     await deleteCompany({ variables: { id } });
  //     showSnackbar('Company deleted successfully', 'success');
  //     refetch();
  //   } catch {
  //     showSnackbar('Failed to delete company', 'error');
  //   }
  // };

  const openDeleteDialog = (id: number) => {
  setCompanyToDelete(id);
  setDeleteDialogOpen(true);
};

const closeDeleteDialog = () => {
  setDeleteDialogOpen(false);
  setCompanyToDelete(null);
};


const confirmDelete = async () => {
  if (companyToDelete !== null) {
    try {
      await deleteCompany({ variables: { id: companyToDelete } });
      showSnackbar('Company deleted successfully', 'success');
      refetch();
    } catch {
      showSnackbar('Failed to delete company', 'error');
    }
  }
  closeDeleteDialog();
};

  if (loading) return <div>Loading...</div>;
  if (error) return <div>Error loading companies.</div>;

  return (
    <>
    <Box p={4}>
      <Typography variant="h4" gutterBottom>Company Management</Typography>
      <Divider />
      <CompanyForm initialData={selectedCompany || undefined} onSubmit={handleSubmit} onCancel={() => setSelectedCompany(null)} />
      <CompanyTable companies={data?.allCompanies || []} onEdit={setSelectedCompany} onDelete={openDeleteDialog} />
    </Box>
     <Snackbar
        open={snackbar.open}
        autoHideDuration={3000}
        onClose={() => setSnackbar((s) => ({ ...s, open: false }))}
        anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
      >
        <Alert
          severity={snackbar.severity}
          variant="filled"
          onClose={() => setSnackbar((s) => ({ ...s, open: false }))}
        >
          {snackbar.message}
        </Alert>
      </Snackbar>

      <Dialog open={deleteDialogOpen} onClose={closeDeleteDialog}>
  <DialogTitle>Confirm Delete</DialogTitle>
  <DialogContent>
    <DialogContentText>Are you sure you want to delete this company?</DialogContentText>
  </DialogContent>
  <DialogActions>
    <Button onClick={closeDeleteDialog} color="primary">Cancel</Button>
    <Button onClick={confirmDelete} color="error" variant="contained">Delete</Button>
  </DialogActions>
</Dialog>
      </>
  );
};

export default CompanyPage;
