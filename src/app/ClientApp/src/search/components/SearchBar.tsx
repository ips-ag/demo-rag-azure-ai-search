import { useState } from 'react';
import { search } from '../services';

export function SearchBar() {
  const [query, setQuery] = useState('');
  const handleSearch = async () => {
    const result = await search(query);
    console.log(result);
  };

  return (
    <div>
      <input type="text" placeholder="Search" value={query} onChange={(e) => setQuery(e.target.value)} />
      <button type="submit" onClick={handleSearch}>
        Search
      </button>
    </div>
  );
}
