import ReactMarkdown from 'react-markdown';
import './SearchResult.css';

interface SearchResultProps {
  text: string;
}

export function SearchResult(props: SearchResultProps) {
  let text = props.text;
  return (
    text &&
    text.trim() != '' && (
      <div className="search-result">
        <ReactMarkdown>{text}</ReactMarkdown>
      </div>
    )
  );
}
