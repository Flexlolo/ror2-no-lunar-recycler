"""
Usage:
  make configure
  make build [options]

Options:
  --release  Build release

Fuck windows
"""
import os
from docopt import docopt

def configure():
	os.environ['MSBuildEnableWorkloadResolver'] = 'false'

	for file in os.listdir('.'):
		if file.endswith('.csproj'):
			os.system(f'nuget restore {file}')
			break


def build(release: bool = False):
	configuration = 'Release' if release else 'Debug'
	configuration = '-property:Configuration=' + configuration
	cmd = 'dotnet msbuild /restore ' + configuration
	print(cmd)
	os.system(cmd)


def main():
	args = docopt(__doc__)

	if args['configure']:
		configure()

	elif args['build']:
		build(args['--release'])

if __name__ == '__main__':
	try:
		main()
	except KeyboardInterrupt:
		print('Interrupted.')
