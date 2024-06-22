import React, { useState, useEffect } from 'react';
import { Container, Grid, Button, Drawer, Box, Pagination } from '@mui/material';
import FilterListIcon from '@mui/icons-material/FilterList';
import ArtworkCard from './profile/ArtworkCard';
import { useAuth } from '../../contexts/AuthContext';
import useArtworks from '../../zsm/stores/useArtworks';
import { AuthorNames, Filter, PagedRequest, SelectedOptions } from '../../types/types';
import { ContentContainer, buttonStyle } from './pagesStyles/ArtworksPage';
import "./pagesStyles/ButtonStyles.css";
import SortPopover from './artworksPage/SortPopover';
import DrawerList from './artworksPage/DrawerList';
import useAuthors from '../../zsm/stores/useAuthors';
import useCategories from '../../zsm/stores/useCategory';
import { useSearchParams } from 'react-router-dom';
import useShoppingCart from '../../zsm/stores/useShoppingCart';



const ArtworksPage = () => {
    const { isLoggedIn, user } = useAuth();
    const artworksStore = useArtworks();
    const authorsStore = useAuthors();
    const categoryStore = useCategories();

    
    const [searchParams] = useSearchParams();
    
    const [open, setOpen] = useState(false);
    const [sortDirection, setSortDirection] = useState<"asc" | "desc">("asc");
    const [anchorEl, setAnchorEl] = useState<HTMLButtonElement | null>(null);
    const [countries, setCountries] = useState<string[] | null>();
    const [names, setNames] = useState<AuthorNames[] | null >();
    const [filterselectedOptions,setFilterselectedOptions] = useState<Filter>();
 

    const [selectedOptions, setSelectedOptions] = useState<SelectedOptions>({
        authors: [],
        priceRange: { min: 0, max: 50000 },
        categories: [],
        countries: [],
        dateRange: null,
      });
   
    const [paginationState, setPaginationState] = useState<PagedRequest>({
        pageIndex: 0,
        pageSize: 10,
        columnNameForSorting: "Price",
        sortDirection: "asc",
        requestFilters: {
            logicalOperator: 0,
            filters: [
                {
                    path: "Price",
                    value: "0",
                    operator: "gt"
                }
            ]
        }
    });

    useEffect(() => {
        const path = searchParams.get('path');
        const value = searchParams.get('value');
        const operator = searchParams.get('operator');
    
        if (path && value && operator) {
            setFilterselectedOptions((prevOptions) => ({
            ...prevOptions,
            path,
            value,
            operator,
          }));
        }
      }, [searchParams]);
    

    useEffect(() => {
        setPaginationState((prevState) => {
            const categoryFilterValue = selectedOptions.categories.join(';');
            const countryFilterValue = selectedOptions.countries.join(';');
            

            const filters = [
                ...selectedOptions.authors.map((author) => [
                    {
                      path: "Author.User.FirstName",
                      value: author.firstName,
                      operator: "contains",
                    },
                    {
                      path: "Author.User.LastName",
                      value: author.lastName,
                      operator: "contains",
                    },
                ]).flat(),
                ...(categoryFilterValue ? [{
                    path: "Category.Name",
                    value: categoryFilterValue,
                    operator: "in",
                }] : []),
                ...(countryFilterValue ? [{
                    path: "Author.Country",
                    value: countryFilterValue,
                    operator: "in",
                }] : []),
                ...(selectedOptions.priceRange ? [
                    {
                        path: "Price",
                        value: `${selectedOptions.priceRange.min};${selectedOptions.priceRange.max}`,
                        operator: 'between',
                    },
                ] : []),
                ...(selectedOptions.dateRange ? [
                    {
                        path: "CreatedDate",
                        value: selectedOptions.dateRange.start,
                        operator: "gt",
                    },
                    {
                        path: "CreatedDate",
                        value: selectedOptions.dateRange.end,
                        operator: "lt",
                    },
                ] : []),
                ...(selectedOptions.dateRange?.start ? [{
                    path: 'CreatedDate',
                    value: selectedOptions.dateRange.start,
                    operator: 'gt_o_eq',
                  }] : []),
                  ...(selectedOptions.dateRange?.end ? [{
                    path: 'CreatedDate',
                    value: selectedOptions.dateRange.end,
                    operator: 'lt_o_eq',
                  }] : []),
                  ...(filterselectedOptions?.path && filterselectedOptions.value && filterselectedOptions.operator
                    ? [
                        {
                          path: filterselectedOptions.path,
                          value: filterselectedOptions.value,
                          operator: filterselectedOptions.operator,
                        },
                      ]
                    : []),
            ].filter(filter => filter !== null); 

            return {
                ...prevState,
                requestFilters: {
                    logicalOperator: 0,
                    filters: filters as any, 
                },
            };
        });
    }, [selectedOptions,filterselectedOptions]);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };

    useEffect(() => {
        setPaginationState((prevState) => ({
            ...prevState,
            pageIndex: 0,
        }));
    }, []);

    useEffect(() =>{
        const fetchAuthorsData = async () =>{
            const countries = await authorsStore.fetchAllCountries();
            const names = await authorsStore.fetchAllAuthorNames();
            await categoryStore.fetchCategories();
            setCountries(countries);       
            setNames(names);       

        };
        fetchAuthorsData();
    },[])

    
    const handleSortDirectionChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        const direction = event.target.value as "asc" | "desc";
        setSortDirection(direction);
        setPaginationState((prevState) => ({
            ...prevState,
            sortDirection: direction,
        }));
        handleClose();
    };

    const handleClose = () => {
        setAnchorEl(null);
    };

    useEffect(() => {
        const fetchArtworks = async () => {
            await artworksStore.fetchPagedArtworks(paginationState);
        };
        fetchArtworks();
    }, [paginationState]);

    function handleDelete(artworkId: number): void {
        
    }

    const toggleDrawer = (newOpen: boolean) => () => {
        setOpen(newOpen);
    };

    const resetFilters = () => {
        setPaginationState({
          pageIndex: 0,
          pageSize: 10,
          columnNameForSorting: "Price",
          sortDirection: "asc",
          requestFilters: {
            logicalOperator: 0,
            filters: [],
          },
        });
        setSelectedOptions({
          authors: [],
          priceRange: { min: 0, max: 50000 },
          categories: [],
          countries: [],
          dateRange: null,
        });
      };

    
    return (
        <>
            <Container maxWidth={false} sx={{
                bgcolor: 'lightgray',
                width: '100%',
                height: '100%',
                border: 'InactiveCaption',
                display: 'flex',
                flexDirection: 'column',
                alignItems: 'center',
                justifyContent: 'center',
            }}>
                <h1>Collect art and design online</h1>

                <Container sx={{ 
                    display: 'flex',
                    alignItems: 'center',
                    justifyContent: 'center'
                    }}
                >
                    <SortPopover
                        anchorEl={anchorEl}
                        handleClick={handleClick}
                        handleClose={handleClose}
                        sortDirection={sortDirection}
                        handleSortDirectionChange={handleSortDirectionChange}
                    />
                    <Button sx={buttonStyle} startIcon={<FilterListIcon />} onClick={toggleDrawer(true)}>All Filters</Button>
                    <Drawer open={open} onClose={toggleDrawer(false)}>
                        <DrawerList authorNames = {names} 
                                    countries = {countries} 
                                    toggleDrawer={toggleDrawer}  
                                    selectedOptions={selectedOptions}
                                    setSelectedOptions={setSelectedOptions} 
                                    resetFilterOptions = {resetFilters}
                        />
                    </Drawer>
                </Container>
                <ContentContainer>
                    <Grid container rowSpacing={2} columnSpacing={{ xs: 1, sm: 2, md: 3 }}>
                        {artworksStore.pagedArtworks?.artworks.map((artwork) => (
                            <Grid item xs={12} sm={6} md={4} lg={3} key={artwork.id}>
                                <ArtworkCard artwork={artwork} onDelete={handleDelete} user={user} />
                            </Grid>
                        ))}
                    </Grid>
                </ContentContainer>
                <Pagination 
                    count={10}
                    sx={{ marginBottom: '20px',
                        '& .MuiPaginationItem-root': {
                            '&:focus': {outline: 'none'}
                        }
                    }}
                    page={paginationState.pageIndex + 1} 
                    onChange={(event, page) => {
                    setPaginationState((prevState) => ({
                    ...prevState,
                    pageIndex: page - 1,    
                    }));
                }}        
                />
            </Container>
        </>
    );
};

export default ArtworksPage;
