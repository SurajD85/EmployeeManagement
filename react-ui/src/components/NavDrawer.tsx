import {
  Drawer,
  List,
  ListItem,
  ListItemIcon,
  ListItemText,
  Toolbar,
  Divider
} from '@mui/material';
import { Dashboard, Business, People } from '@mui/icons-material';
import { Link, useLocation } from 'react-router-dom';

const NavDrawer = ({ drawerWidth }: { drawerWidth: number }) => {
  const location = useLocation();

  const navItems = [
    { text: 'Dashboard', icon: <Dashboard />, path: '/' },
    { text: 'Company', icon: <Business />, path: '/company' },
    { text: 'Employee', icon: <People />, path: '/employee' },
  ];

  return (
    <Drawer
      variant="permanent"
      sx={{
        width: drawerWidth,
        flexShrink: 0,
        [`& .MuiDrawer-paper`]: {
          width: drawerWidth,
          boxSizing: 'border-box',
        },
      }}
    >
      <Toolbar />
      <Divider />
      <List>
        {navItems.map(({ text, icon, path }) => {
          const isSelected =
            path === '/'
              ? location.pathname === '/'
              : location.pathname.startsWith(path);

          return (
            <ListItem
              button
              key={text}
              component={Link}
              to={path}
              selected={isSelected}
              sx={{
                '&.Mui-selected': {
                  backgroundColor: (theme) =>
                    theme.palette.mode === 'light' ? '#e0e0e0' : '#424242',
                  borderLeft: '4px solid #1976d2',
                  paddingLeft: '12px',
                  '&:hover': {
                    backgroundColor: (theme) =>
                      theme.palette.mode === 'light' ? '#d5d5d5' : '#333',
                  },
                },
                '&:hover': {
                  backgroundColor: (theme) =>
                    theme.palette.mode === 'light' ? '#f5f5f5' : '#2c2c2c',
                },
              }}
            >
              <ListItemIcon>{icon}</ListItemIcon>
              <ListItemText primary={text} />
            </ListItem>
          );
        })}
      </List>
    </Drawer>
  );
};

export default NavDrawer;
