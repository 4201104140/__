import path from 'path';
import webpack, { library } from 'webpack';
import { capitalize } from 'underscore.string'

const ROOT = path.resolve(__dirname, 'src')
const DESTINATION = path.resolve(__dirname, 'dist')
const libraryName: string = 'pagetour'

const PATHS = {
  ROOT,
  DESTINATION,
  MAIN_TS: './index.ts',
  MAIN_SASS: './styles/pagetour-styles.scss'
}

const isDev = (env: any) => env && env.development;

const tsBuildModules: webpack.ModuleOptions = {
  rules: [
    /****************
     * PRE-LOADERS
     *****************/
    {
      enforce: 'pre',
      test: /\.js$/,
      use: 'source-map-loader'
    },

    /****************
     * LOADERS
     *****************/

    {
      test: /\.html$/,
      use: ['handlebars-loader', 'html-minify-loader']
    },
    {
      test: /\.ts$/,
      exclude: [/node_modules/],
      use: [
        {
          loader: 'ts-loader',
          options: {
            configFile: 'tsconfig.json'
          }
        }
      ]
    }
  ]
}

const baseConfig = (env: any): webpack.Configuration => {
  return {
    // optimization: optimization(env),
    context: PATHS.ROOT,
    mode: isDev(env) ? 'development' : 'production',
    entry: {
      pagetour: PATHS.MAIN_TS,
    },
    resolve: {
      alias: {
        '@src': PATHS.ROOT,
        handlebars: 'handlebars/dist/handlebars.runtime.min.js'
      },
      extensions: ['.ts', '.js'],
      modules: [ROOT, 'node_modules', '../../node_modules']
    },
    module: tsBuildModules,
    devtool: 'cheap-module-source-map'
  }
}

const umdConfig = (env: any): webpack.Configuration => {
  return {
    ...baseConfig,
    output: {
      filename: '[name].umd.js',
      chunkFilename: '[name].umd.js',
      path: PATHS.DESTINATION + '/umd',
      sourceMapFilename: '[file].map',
      libraryTarget: 'umd',
      library: capitalize(libraryName)
    }
  }
}

const iifeConfig = (env: any): webpack.Configuration => {
  return {
    ...baseConfig(env),
    output: {
      filename: '[name].iife.js',
      chunkFilename: '[name].iife.js',
      path: PATHS.DESTINATION + '/iife',
      sourceMapFilename: '[file].map',
      libraryTarget: 'var',
      library: capitalize(libraryName)
    }
  }
}

const config = (env: any) => {
  return [umdConfig(env), iifeConfig(env)]
}

export default config;