// components/CompanyTable.tsx
import React from 'react';
import { Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Paper, IconButton } from '@mui/material';
import { Edit, Delete } from '@mui/icons-material';
import type { Company } from '../../types/company';

// interface Company {
//   id: number;
//   name: string;
//   emailAddress?: string;
//   phoneNumber?: string;
//   establishedDate: string;
// }

interface Props {
  companies: Company[];
  onEdit: (company: Company) => void;
  onDelete: (id: number) => void;
}

const CompanyTable: React.FC<Props> = ({ companies, onEdit, onDelete }) => {
  return (
    <TableContainer component={Paper} sx={{ mt: 4 }}>
      <Table>
        <TableHead>
          <TableRow>
            <TableCell>Name</TableCell>
            <TableCell>Email</TableCell>
            <TableCell>Phone</TableCell>
            <TableCell>Established</TableCell>
            <TableCell align="right">Actions</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {companies.map((company) => (
            <TableRow key={company.id}>
              <TableCell>{company.name}</TableCell>
              <TableCell>{company.emailAddress}</TableCell>
              <TableCell>{company.phoneNumber}</TableCell>
              <TableCell>{company.establishedDate?.split('T')[0]}</TableCell>
              <TableCell align="right">
                <IconButton onClick={() => onEdit(company)}><Edit /></IconButton>
                <IconButton onClick={() => company.id !== undefined && onDelete(company.id)}><Delete /></IconButton>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
    </TableContainer>
  );
};

export default CompanyTable;
