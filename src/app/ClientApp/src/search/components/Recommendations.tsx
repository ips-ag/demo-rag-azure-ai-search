import './Recommendations.css';

interface RecommendationsProps {
  phrases: string[];
  onPhraseSelect: (phrase: string) => void;
}

export function Recommendations(props: RecommendationsProps) {
  return (
    <div className="recommendations">
      {props.phrases.map((phrase, index) => (
        <button
          key={index}
          className="recommendations-item"
          onClick={() => props.onPhraseSelect(phrase)}>
          {phrase}
        </button>
      ))}
    </div>
  );
}
