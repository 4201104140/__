import camelcase from 'camelcase';
import { TableSpec } from '../SchemaInterface';
import { knexColumnSpec, typescriptColumnSpec, typescriptType } from './ColumnGenerators';

export function typescriptTableName(name: string): string {
  return camelcase(name, { pascalCase: true });
}

