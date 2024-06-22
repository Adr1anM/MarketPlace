import * as React from 'react';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';
import Stack from '@mui/material/Stack';
import { AuthorNames } from '../../../types/types';

interface AuthorsNameProps {
  authors: AuthorNames[];
  onAuthorSelect: (author: AuthorNames | null) => void;
}

const AuthorsName: React.FC<AuthorsNameProps> = ({ authors ,onAuthorSelect}) => {
  const defaultProps = {
    options: authors,
    getOptionLabel: (option: AuthorNames) => `${option.firstName} ${option.lastName}`,
  };

  const [value, setValue] = React.useState<AuthorNames | null>(null);

  return (
    <Stack spacing={1} sx={{ width: 300 }}>
      <Autocomplete
        {...defaultProps}
        id="disable-close-on-select"
        disableCloseOnSelect
        renderInput={(params) => (
          <TextField {...params} label="Authors" variant="standard" />
        )}
        value={value}
        onChange={(event, newValue) => {
          setValue(newValue);
          onAuthorSelect(newValue);
        }}
      />
    </Stack>
  );
}

export default AuthorsName;
