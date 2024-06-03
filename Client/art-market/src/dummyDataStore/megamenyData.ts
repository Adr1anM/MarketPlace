import { MegaMenuSection } from "../components/layouts/navbar/megaMenues/MegaMenu";


export const artistsSections: MegaMenuSection[] = [
    {
      title: 'By Style',
      links: [
        { text: 'Abstract', href: '/artists/style/abstract' },
        { text: 'Contemporary', href: '/artists/style/contemporary' },
        { text: 'Modern', href: '/artists/style/modern' },
        { text: 'Pop Art', href: '/artists/style/pop-art' },
      ],
    },
    {
      title: 'By Country',
      links: [
        { text: 'American', href: '/artists/country/american' },
        { text: 'French', href: '/artists/country/french' },
        { text: 'Italian', href: '/artists/country/italian' },
        { text: 'Japanese', href: '/artists/country/japanese' },
      ],
    },
    {
      title: 'Featured',
      links: [
        { text: 'Rising Stars', href: '/artists/featured/rising-stars' },
        { text: 'Award Winners', href: '/artists/featured/award-winners' },
        { text: 'Most Popular', href: '/artists/featured/most-popular' },
      ],
    },
  ];
  
  export const artworksSections: MegaMenuSection[] = [
    {
      title: 'By Medium',
      links: [
        { text: 'Oil Painting', href: '/artworks/medium/oil' },
        { text: 'Watercolor', href: '/artworks/medium/watercolor' },
        { text: 'Sculpture', href: '/artworks/medium/sculpture' },
        { text: 'Digital Art', href: '/artworks/medium/digital' },
      ],
    },
    {
      title: 'By Subject',
      links: [
        { text: 'Landscape', href: '/artworks/subject/landscape' },
        { text: 'Portrait', href: '/artworks/subject/portrait' },
        { text: 'Abstract', href: '/artworks/subject/abstract' },
        { text: 'Still Life', href: '/artworks/subject/still-life' },
      ],
    },
    {
      title: 'By Price',
      links: [
        { text: 'Under $500', href: '/artworks/price/under-500' },
        { text: '$500 - $2,000', href: '/artworks/price/500-2000' },
        { text: '$2,000 - $10,000', href: '/artworks/price/2000-10000' },
        { text: 'Over $10,000', href: '/artworks/price/over-10000' },
      ],
    },
  ];