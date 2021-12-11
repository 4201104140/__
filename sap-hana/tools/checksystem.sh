#!/bin/bash

# Set global exit code
EXIT_STATUS=0

# Boot configuration files
BOOT_INSTALLDEVICE_FILENAME="/etc/default/grub_installdevice"
BOOT_DEVICEMAP_FILENAME="/boot/grub2/device.map"

checkBootConfig()
{
  # Note: Handling of these files are tricky, as it's not clear when files exist/
  #
  # 1. We known that if the files don't exist in SP3, we can upgrade to SP4

  # Check if boot configuration files exist
  if [ ! -e ${BOOT_INSTALLDEVICE_FILENAME} ] || [ ! -e ${BOOT_DEVICEMAP_FILENAME} ]; then
    echo "Note: Boot configuration files don't exist; skipping boot checks" 1>&2
    return
  fi 

  # Get the boot LUN
  BOOT_LUN=$(blkid | grep /dev/mapper | head -1 | cut -f1 -d: | cut -f1 -d- )

  # Check /etc/default/grub_installdevice
  BOOT_INSTALLDEVICE=$(head -1 < ${BOOT_INSTALLDEVICE_FILENAME})
  if [ "${BOOT_INSTALLDEVICE}" != "${BOOT_LUN}" ]; then
    echo "ERROR: File ${BOOT_INSTALLDEVICE_FILENAME} is not configured with correct boot LUN" 1>&2
    EXIT_STATUS=1
  fi
}

checkBootConfig

exit ${EXIT_STATUS}