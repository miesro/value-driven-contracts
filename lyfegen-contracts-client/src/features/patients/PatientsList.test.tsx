import { describe, it, expect } from 'vitest';
import { render, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import PatientsList from './PatientsList';
import type { PatientListItemDto } from '../../api/patients.types';

describe('PatientsList', () => {
  it('renders patient names as links to their details', () => {
    const items: PatientListItemDto[] = [
      { id: 1, name: 'Alice Smith' },
      { id: 2, name: 'Bob Jones' },
    ];

    render(
      <MemoryRouter>
        <PatientsList items={items} />
      </MemoryRouter>
    );

    // Empty state should not appear
    expect(screen.queryByText(/No patients/i)).toBeNull();

    // Each patient is a link with the right href
    for (const p of items) {
      const link = screen.getByRole('link', { name: p.name });
      expect(link).toBeInTheDocument();
      expect(link).toHaveAttribute('href', `/patients/${p.id}`);
    }

    // Count should match
    expect(screen.getAllByRole('link')).toHaveLength(items.length);
  });
});
