import { MegaMenuSection } from "../components/layouts/navbar/megaMenues/MegaMenu";
import { Filter } from "../types/types";


export const artistsSections: MegaMenuSection[] = [
    {
      title: 'By Style',
      links: [
        { text: 'Abstract', href: '/artists/style/abstract',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Contemporary', href: '/artists/style/contemporary',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Modern', href: '/artists/style/modern',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Pop Art', href: '/artists/style/pop-art',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
      ],
    },
    {
      title: 'By Country',
      links: [
        { text: 'American', href: '/artists/country/american',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'French', href: '/artists/country/french',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Italian', href: '/artists/country/italian',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Japanese', href: '/artists/country/japanese',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
      ],
    },
    {
      title: 'Featured',
      links: [
        { text: 'Rising Stars', href: '/artists/featured/rising-stars',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Award Winners', href: '/artists/featured/award-winners',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Most Popular', href: '/artists/featured/most-popular',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
      ],
    },
  ];

  const filterOptions : Filter = {
    path: '',
    value: '',
    operator: ''
  };
  
  export const artworksSections: MegaMenuSection[] = [
    
    {
      title: 'By Medium',
      links: [
        { text: 'Drawing', href: '/artworks/medium/oil',filterOptions: { path: 'Category.Name', value: 'Drawing', operator: 'contains' } },
        { text: 'Paint', href: '/artworks/medium/watercolor' ,filterOptions: { path: 'Category.Name', value: 'Paint', operator: 'contains' }},
        { text: 'Sculpture', href: '/artworks/medium/sculpture',filterOptions: { path: 'Category.Name', value: 'Sculpture', operator: 'contains' } },
        { text: 'Photography', href: '/artworks/medium/digital',filterOptions: { path: 'Category.Name', value: 'Photography', operator: 'contains' } },
      ],
    },
    {
      title: 'By Subject',
      links: [
        { text: 'Landscape', href: '/artworks/subject/landscape', filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Portrait', href: '/artworks/subject/portrait' ,filterOptions: { path: 'Price', value: '0;500', operator: 'between' }},
        { text: 'Abstract', href: '/artworks/subject/abstract',filterOptions: { path: 'Price', value: '0;500', operator: 'between' } },
        { text: 'Contemporary', href: '/artworks/subject/still-life' ,filterOptions: { path: 'Price', value: '0;500', operator: 'between' }},
      ],
    },
    {
      title: 'By Price',
    links: [
      { text: 'Under $500', href: '/artworks/price/under-500', filterOptions: { path: 'Price', value: '0;500', operator: 'between' }},
      { text: '$500 - $2,000', href: '/artworks/price/500-2000', filterOptions: { path: 'Price', value: '500;2000', operator: 'between' } },
      { text: '$2,000 - $10,000', href: '/artworks/price/2000-10000', filterOptions: { path: 'Price', value: '2000;10000', operator: 'between' } },
      { text: 'Over $10,000', href: '/artworks/price/over-10000', filterOptions: { path: 'Price', value: '10000', operator: 'gt' } },
    ],
    },
  ];